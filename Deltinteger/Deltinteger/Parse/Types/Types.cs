using System;
using System.Collections.Generic;
using System.Linq;
using Deltin.Deltinteger.Elements;
using Deltin.Deltinteger.LanguageServer;
using CompletionItem = OmniSharp.Extensions.LanguageServer.Protocol.Models.CompletionItem;
using CompletionItemKind = OmniSharp.Extensions.LanguageServer.Protocol.Models.CompletionItemKind;
using StringOrMarkupContent = OmniSharp.Extensions.LanguageServer.Protocol.Models.StringOrMarkupContent;

namespace Deltin.Deltinteger.Parse
{
    public abstract class CodeType : IExpression, ICallable
    {
        public string Name { get; }
        public Constructor[] Constructors { get; protected set; } = new Constructor[0];
        public CodeType Extends { get; set; }
        public string Description { get; protected set; }
        protected string Kind = "class";

        /// <summary>Determines if the class can be deleted with the delete keyword.</summary>
        public bool CanBeDeleted { get; protected set; } = false;

        /// <summary>Determines if other classes can inherit this class.</summary>
        public bool CanBeExtended { get; protected set; } = false;

        public CodeType(string name)
        {
            Name = name;
        }

        public virtual bool DoesImplement(CodeType type)
        {
            if (type == null) return false;
            if (type == this) return true;
            if (Extends.DoesImplement(type)) return true;
            return false;
        }

        // Static
        public abstract Scope ReturningScope();
        // Object
        public virtual Scope GetObjectScope() => null;

        public CodeType Type() => null;
        public IWorkshopTree Parse(ActionSet actionSet) => null;

        /// <summary>Determines if variables with this type can have their value changed.</summary>
        public virtual TypeSettable Constant() => TypeSettable.Normal;

        /// <summary>The returning value when `new TypeName` is called.</summary>
        /// <param name="actionSet">The actionset to use.</param>
        /// <param name="constructor">The constuctor that was called.</param>
        /// <param name="constructorValues">The parameter values of the constructor.</param>
        /// <param name="additionalParameterData">Additional parameter data.</param>
        public virtual IWorkshopTree New(ActionSet actionSet, Constructor constructor, IWorkshopTree[] constructorValues, object[] additionalParameterData)
        {
            // Classes that can't be created shouldn't have constructors.
            throw new NotImplementedException();
        }
        
        /// <summary>Sets up an object reference when a new object is created. Is also called when a new object of a class extending this type is created.</summary>
        /// <param name="actionSet">The actionset to use.</param>
        /// <param name="reference">The reference of the object.</param>
        public virtual void BaseSetup(ActionSet actionSet, Element reference) => throw new NotImplementedException();

        /// <summary>Resolve code actions.</summary>
        public virtual void ResolveElements() {}

        /// <summary>Assigns workshop elements so the class can function. Implementers should check if `wasCalled` is true.</summary>
        public virtual void WorkshopInit(DeltinScript translateInfo) {}

        /// <summary>Adds the class objects to the index assigner.</summary>
        /// <param name="source">The source of the type.</param>
        /// <param name="assigner">The assigner that the object variables will be added to.</param>
        public virtual void AddObjectVariablesToAssigner(IWorkshopTree reference, VarIndexAssigner assigner) {}

        /// <summary>Deletes a variable from memory.</summary>
        /// <param name="actionSet">The action set to add the actions to.</param>
        /// <param name="reference">The object reference.</param>
        public virtual void Delete(ActionSet actionSet, Element reference) {}

        /// <summary>Calls a type from the specified document range.</summary>
        /// <param name="script">The script that the type was called from.</param>
        /// <param name="callRange">The range of the call.</param>
        public virtual void Call(ScriptFile script, DocRange callRange)
        {
            script.AddHover(callRange, HoverHandler.Sectioned(Kind + " " + Name, Description));
        }

        /// <summary>Gets the completion that will show up for the language server.</summary>
        public abstract CompletionItem GetCompletion();

        public static CodeType GetCodeTypeFromContext(ParseInfo parseInfo, DeltinScriptParser.Code_typeContext typeContext)
        {
            if (typeContext == null) return null;
            CodeType type = parseInfo.TranslateInfo.Types.GetCodeType(typeContext.PART().GetText(), parseInfo.Script.Diagnostics, DocRange.GetRange(typeContext));

            // Get generics
            if (typeContext.generics()?.generic_option() != null)
            {
                // Create a list to store the generics.
                List<CodeType> generics = new List<CodeType>();

                // Get the generics.
                foreach (var genericContext in typeContext.generics().generic_option())
                {
                    if (genericContext.DEFINE() != null)
                        generics.Add(null);
                    else
                        generics.Add(GetCodeTypeFromContext(parseInfo, genericContext.code_type()));
                }
                
                if (type is Lambda.BlockLambda)
                    type = new Lambda.BlockLambda(generics.ToArray());
                else if (type is Lambda.ValueBlockLambda)
                    type = new Lambda.ValueBlockLambda(generics[0], generics.Skip(1).ToArray());
                else if (type is Lambda.MacroLambda)
                    type = new Lambda.MacroLambda(generics[0], generics.Skip(1).ToArray());
            }

            if (type != null)
            {
                type.Call(parseInfo.Script, DocRange.GetRange(typeContext));

                if (typeContext.INDEX_START() != null)
                    for (int i = 0; i < typeContext.INDEX_START().Length; i++)
                        type = new ArrayType(type);
            }
            return type;
        }

        static List<CodeType> _defaultTypes;
        public static List<CodeType> DefaultTypes {
            get {
                if (_defaultTypes == null) GetDefaultTypes();
                return _defaultTypes;
            }
        }
        private static void GetDefaultTypes()
        {
            _defaultTypes = new List<CodeType>();
            foreach (var enumData in EnumData.GetEnumData())
                if (enumData.ConvertableToElement())
                    _defaultTypes.Add(new ValueGroupType(enumData));
                else
                    _defaultTypes.Add(new WorkshopEnumType(enumData));
            
            // Add custom classes here.
            _defaultTypes.Add(new Pathfinder.PathmapClass());
            _defaultTypes.Add(new Models.AssetClass());
            _defaultTypes.Add(new Lambda.BlockLambda());
            _defaultTypes.Add(new Lambda.ValueBlockLambda());
            _defaultTypes.Add(new Lambda.MacroLambda());
            _defaultTypes.Add(VectorType.Instance);
        }
    }

    public enum TypeSettable
    {
        Normal, Convertable, Constant
    }
}