using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock
{
    internal static class Routing
    {
        internal const string PREFIX = "Warlock";
        internal static class Endpoints
        {

            public const string EMU_SIDE = PREFIX + "_" + "EMU";
            public const string RTC_SIDE = PREFIX + "_" + "RTC";
        }

        /// <summary>
        /// Add your commands here
        /// </summary>
        internal static class Commands
        {
            public const string APPLY_BLAST_LAYER = PREFIX + "_" + nameof(APPLY_BLAST_LAYER);
            public const string UNDO_BLAST_LAYER = PREFIX + "_" + nameof(UNDO_BLAST_LAYER);

            public const string UPDATE_SPEC = PREFIX + "_" + nameof(UPDATE_SPEC);
            public const string LOAD_SCRIPT = PREFIX + "_" + nameof(LOAD_SCRIPT);
            public const string LOAD_NAMED_SCRIPT = PREFIX + "_" + nameof(LOAD_NAMED_SCRIPT);
            public const string LOAD_GLOBAL_SCRIPT = PREFIX + "_" + nameof(LOAD_GLOBAL_SCRIPT);
            public const string LOAD_GLOBAL_SCRIPTS = PREFIX + "_" + nameof(LOAD_GLOBAL_SCRIPTS);
            public const string LOAD_STASHKEY = PREFIX + "_" + nameof(LOAD_STASHKEY);
            public const string LOAD_STASHKEY_SAVESTATE = PREFIX + "_" + nameof(LOAD_STASHKEY_SAVESTATE);
            public const string LOAD_INTERNAL_SAVESTATE = PREFIX + "_" + nameof(LOAD_INTERNAL_SAVESTATE);
            public const string SAVE_INTERNAL_SAVESTATE = PREFIX + "_" + nameof(SAVE_INTERNAL_SAVESTATE);
            public const string EXECUTE_HOOK = PREFIX + "_" + nameof(EXECUTE_HOOK);
            public const string EXECUTE_HOOK_NO_INTERRUPT = PREFIX + "_" + nameof(EXECUTE_HOOK_NO_INTERRUPT);
            public const string RESET = PREFIX + "_" + nameof(RESET);
            public const string RUN = PREFIX + "_" + nameof(RUN);
            public const string STOP = PREFIX + "_" + nameof(STOP);
        }
        
    }
}
