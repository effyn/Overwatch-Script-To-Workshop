﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Deltin.Deltinteger.Elements;

namespace Deltin.Deltinteger
{
    public static class Constants
    {
        public const int RULE_NAME_MAX_LENGTH = 128;

        public const double MINIMUM_WAIT = 0.016;

        public const string INTERNAL_ELEMENT = "INTERNAL : ";

        public const string DEFAULT_STRING = "hello";

        public const int MAX_ARRAY_LENGTH = 1000;

        public static readonly string[] Strings = new string[]
        {
            // All lowercase please :)
            "",
            "----------",
            "-> {0}",
            "!",
            "!!",
            "!!!",
            "#{0}",
            "({0})",
            "*",
            "...",
            "?",
            "??",
            "???",
            "{0} - {1}",
            "{0} - {1} - {2}",
            "{0} ->",
            "{0} -> {1}",
            "{0} != {1}",
            "{0} * {1}",
            "{0} / {1}",
            "{0} : {1} : {2}",
            "{0} {1}",
            "{0} {1} {2}",
            "{0} + {1}",
            "{0} <-",
            "{0} <- {1}",
            "{0} <->",
            "{0} <-> {1}",
            "{0} < {1}",
            "{0} <= {1}",
            "{0} = {1}",
            "{0} == {1}",
            "{0} > {1}",
            "{0} >= {1}",
            "{0} and {1}",
            "{0} m",
            "{0} m/s",
            "{0} sec",
            "{0} vs {1}",
            "{0}!",
            "{0}!!",
            "{0}!!!",
            "{0}%",
            "{0}, {1}",
            "{0}, {1}, and {2}",
            "{0}:",
            "{0}: {1}",
            "{0}: {1} and {2}",
            "{0}:{1}",
            "{0}?",
            "{0}??",
            "{0}???",
            "<- {0}",
            "<-> {0}",
            "abilities",
            "ability",
            "ability 1",
            "ability 2",
            "alert",
            "alive",
            "allies",
            "ally",
            "ammunition",
            "angle",
            "attack",
            "attacked",
            "attacking",
            "attempt",
            "attempts",
            "average",
            "avoid",
            "avoided",
            "avoiding",
            "backward",
            "bad",
            "ban",
            "banned",
            "banning",
            "best",
            "better",
            "bid",
            "bids",
            "block",
            "blocked",
            "blocking",
            "blue",
            "bonus",
            "bonuses",
            "boss",
            "bosses",
            "bought",
            "build",
            "building",
            "built",
            "burn",
            "burning",
            "burnt",
            "buy",
            "buying",
            "capture",
            "captured",
            "capturing",
            "caution",
            "center",
            "challenge_accepted",
            "charisma",
            "chase",
            "chased",
            "chasing",
            "checkpoint",
            "checkpoints",
            "cloud",
            "clouds",
            "club",
            "clubs",
            "combo",
            "come_here",
            "condition",
            "congratulations",
            "connect",
            "connected",
            "connecting",
            "constitution",
            "control_point",
            "control_points",
            "cooldown",
            "cooldowns",
            "corrupt",
            "corrupted",
            "corrupting",
            "credit",
            "credits",
            "critical",
            "crouch",
            "crouched",
            "crouching",
            "current",
            "current_allies",
            "current_ally",
            "current_attempt",
            "current_checkpoint",
            "current_enemies",
            "current_enemy",
            "current_form",
            "current_game",
            "current_hero",
            "current_heroes",
            "current_hostage",
            "current_hostages",
            "current_level",
            "current_mission",
            "current_object",
            "current_objective",
            "current_objects",
            "current_phase",
            "current_player",
            "current_players",
            "current_round",
            "current_target",
            "current_targets",
            "current_upgrade",
            "damage",
            "damaged",
            "damaging",
            "danger",
            "dead",
            "deal",
            "dealing",
            "dealt",
            "deck",
            "decks",
            "defeat",
            "defend",
            "defended",
            "defending",
            "defence",
            "deliver",
            "delivered",
            "delivering",
            "depth",
            "destabilize",
            "destabilized",
            "destabilizing",
            "destroy",
            "destroyed",
            "destroying",
            "detect",
            "detected",
            "detecting",
            "dexterity",
            "diamond",
            "diamonds",
            "die",
            "discard",
            "discarded",
            "discarding",
            "disconnect",
            "disconnected",
            "disconnecting",
            "distance",
            "distances",
            "dodge",
            "dodged",
            "dodging",
            "dome",
            "domes",
            "down",
            "download",
            "downloaded",
            "downloading",
            "draw",
            "drawing",
            "drawn",
            "drop",
            "dropped",
            "dropping",
            "dying",
            "east",
            "eliminate",
            "eliminated",
            "eliminating",
            "elimination",
            "eliminations",
            "enemies",
            "enemy",
            "entrance",
            "escort",
            "escorted",
            "escorting",
            "excellent",
            "exit",
            "experience",
            "extreme",
            "face",
            "faces",
            "facing",
            "failed",
            "failing",
            "failure",
            "fall",
            "fallen",
            "falling",
            "far",
            "fast",
            "faster",
            "fastest",
            "fault",
            "faults",
            "final",
            "final_allies",
            "final_ally",
            "final_attempt",
            "final_checkpoint",
            "final_enemies",
            "final_enemy",
            "final_form",
            "final_game",
            "final_hero",
            "final_heroes",
            "final_hostage",
            "final_hostages",
            "final_item",
            "final_level",
            "final_mission",
            "final_object",
            "final_objective",
            "final_objects",
            "final_phase",
            "final_player",
            "final_players",
            "final_round",
            "final_target",
            "final_targets",
            "final_time",
            "final_upgrade",
            "find",
            "finding",
            "finish",
            "finished",
            "finishing",
            "flown",
            "fly",
            "flying",
            "fold",
            "folded",
            "folding",
            "form",
            "forms",
            "forward",
            "found",
            "freeze",
            "freezing",
            "frozen",
            "game",
            "games",
            "games_lost",
            "games_won",
            "gg",
            "go",
            "goal",
            "goals",
            "going",
            "good",
            "good_luck",
            "goodbye",
            "green",
            "guilty",
            "hack",
            "hacked",
            "hacking",
            "hand",
            "hands",
            "heal",
            "healed",
            "healer",
            "healers",
            "healing",
            "heart",
            "hearts",
            "height",
            "hello",
            "help",
            "here",
            "hero",
            "heroes",
            "hidden",
            "hide",
            "hiding",
            "high_score",
            "high_scores",
            "hit",
            "hitting",
            "hmmm",
            "hostage",
            "hostages",
            "huh",
            "hunt",
            "hunted",
            "hunter",
            "hunters",
            "hunting",
            "i_give_up",
            "i_tried",
            "in_view",
            "income",
            "incoming",
            "initial",
            "initial_allies",
            "initial_ally",
            "initial_attempt",
            "initial_checkpoint",
            "initial_enemies",
            "initial_enemy",
            "initial_form",
            "initial_game",
            "initial_hero",
            "initial_heroes",
            "initial_hostage",
            "initial_level",
            "initial_mission",
            "initial_object",
            "initial_objective",
            "initial_objects",
            "initial_phase",
            "initial_player",
            "initial_players",
            "initial_round",
            "initial_target",
            "initial_targets",
            "initial_upgrade",
            "innocent",
            "inside",
            "intelligence",
            "interact",
            "invisible",
            "item",
            "items",
            "join",
            "joined",
            "joining",
            "jump",
            "jumping",
            "kill",
            "kills",
            "killstreak",
            "killstreak",
            "killstreaks",
            "leader",
            "leaders",
            "least",
            "left",
            "less",
            "level",
            "level_down",
            "level_up",
            "levels",
            "life",
            "limited",
            "lives",
            "load",
            "loaded",
            "loading",
            "location",
            "lock",
            "locked",
            "locking",
            "loser",
            "losers",
            "loss",
            "losses",
            "max",
            "mild",
            "min",
            "mission",
            "mission", /* what? */
            "mission_aborted",
            "mission_accomplished",
            "mission_failed",
            "missions",
            "moderate",
            "money",
            "monster",
            "monsters",
            "more",
            "most",
            "my_mistake",
            "near",
            "new_high_score",
            "new_record",
            "next",
            "next_allies",
            "next_ally",
            "next_attempt",
            "next_checkpoint",
            "next_enemies",
            "next_enemy",
            "next_form",
            "next_game",
            "next_hero",
            "next_heroes",
            "next_hostage",
            "next_hostages",
            "next_level",
            "next_mission",
            "next_object",
            "next_objective",
            "next_objects",
            "next_phase",
            "next_player",
            "next_players",
            "next_round",
            "next_target",
            "next_targets",
            "next_upgrade",
            "nice_try",
            "no",
            "no_thanks",
            "none",
            "normal",
            "north",
            "northeast",
            "northwest",
            "not_today",
            "object",
            "objective",
            "objectives",
            "objects",
            "obtain",
            "obtained",
            "obtaining",
            "off",
            "on",
            "oof",
            "oops",
            "optimal",
            "optimize",
            "optimized",
            "optimizing",
            "out_of_view",
            "outgoing",
            "outside",
            "over",
            "overtime",
            "participant",
            "participants",
            "payload",
            "payloads",
            "phase",
            "phases",
            "pick",
            "picked",
            "picking",
            "pile",
            "piles",
            "play",
            "played",
            "player",
            "players",
            "playing",
            "point",
            "points",
            "points_earned",
            "points_lost",
            "position",
            "power",
            "power-up",
            "power-ups",
            "price",
            "primary_fire",
            "projectile",
            "projectiles",
            "protect",
            "protected",
            "protecting",
            "purified",
            "purify",
            "purifying",
            "purple",
            "raise",
            "raised",
            "raising",
            "rank",
            "rank_a",
            "rank_b",
            "rank_c",
            "rank_d",
            "rank_e",
            "rank_f",
            "rank_s",
            "reach",
            "reached",
            "reaching",
            "ready",
            "record",
            "records",
            "recover",
            "recovered",
            "recovering",
            "red",
            "remain",
            "remaining",
            "rescue",
            "rescued",
            "rescuing",
            "resource",
            "resources",
            "resurrect",
            "resurrected",
            "resurrecting",
            "reveal",
            "revealed",
            "revealing",
            "right",
            "round",
            "round {0}",
            "rounds",
            "rounds_lost",
            "rounds_won",
            "run",
            "running",
            "safe",
            "save",
            "saved",
            "saving",
            "score",
            "scores",
            "secondary_fire",
            "secure",
            "secured",
            "securing",
            "select",
            "selected",
            "selecting",
            "sell",
            "selling",
            "server_load",
            "server_load_average",
            "server_load_peak",
            "sever",
            "severe",
            "severed",
            "severing",
            "shop",
            "shops",
            "shuffle",
            "shuffled",
            "sink",
            "sinking",
            "skip",
            "skipped",
            "skipping",
            "sleep",
            "sleeping",
            "slept",
            "slow",
            "slower",
            "slowest",
            "sold",
            "sorry",
            "south",
            "southeast",
            "southwest",
            "spade",
            "spades",
            "sparkles",
            "spawn",
            "spawned",
            "spawning",
            "speed",
            "speeds",
            "sphere",
            "spheres",
            "stabilize",
            "stabilized",
            "stabilizing",
            "stable",
            "star",
            "stars",
            "start",
            "started",
            "starting",
            "status",
            "stay",
            "stay_away",
            "stayed",
            "staying",
            "stop",
            "stopped",
            "stopping",
            "strength",
            "stun",
            "stunned",
            "stunning",
            "suboptimal",
            "success",
            "sudden_death",
            "sunk",
            "superb",
            "survive",
            "survived",
            "surviving",
            "target",
            "targets",
            "team",
            "teammate",
            "teammates",
            "teams",
            "terrible",
            "thank_you",
            "thanks",
            "that_was_awesome",
            "threat",
            "threat_level",
            "threat_levels",
            "threats",
            "tiebreaker",
            "time",
            "times",
            "total",
            "trade",
            "traded",
            "trading",
            "traitor",
            "traitors",
            "transfer",
            "transferred",
            "transferring",
            "try_again",
            "turret",
            "turrets",
            "ugh",
            "ultimate_ability",
            "under",
            "unknown",
            "unlimited",
            "unlock",
            "unlocked",
            "unlocking",
            "unsafe",
            "unstable",
            "up",
            "upgrade",
            "upgrades",
            "upload",
            "uploaded",
            "uploading",
            "use_ability 1",
            "use_ability 2",
            "use_ultimate_ability",
            "victory",
            "visible",
            "vortex",
            "vortices",
            "wait",
            "waiting",
            "wall",
            "walls",
            "warning",
            "welcome",
            "well_played",
            "west",
            "white",
            "wild",
            "win",
            "winner",
            "winners",
            "wins",
            "wisdom",
            "worse",
            "worst",
            "wow",
            "yellow",
            "yes",
            "you",
            "you_lose",
            "you_win",
            "zone",
            "zones",
            "¡{0}!",
            "¿{0}?"
        };

        public static readonly string[] AllOperations = { 
            "^", "*", "%", "/", "+", "-", 
            "<", "<=", "==", ">=", ">", "!=",
            "&&", "||" 
        };
        public static readonly string[] MathOperations = new string[] { "^", "*", "%", "/", "+", "-" };
        public static readonly string[] CompareOperations = new string[] { "<", "<=", "==", ">=", ">", "!=" };
        public static readonly string[] BoolOperations = new string[] { "&&", "||" };
    }
}
