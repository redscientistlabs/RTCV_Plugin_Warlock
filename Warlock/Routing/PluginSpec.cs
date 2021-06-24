using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTCV.NetCore;
using RTCV.PluginHost;
using LunarBind;

namespace Warlock
{
    [LunarBindPrefix("Spec")]
    static class PluginSpec
    {
        static Dictionary<string, object> spec = new Dictionary<string, object>();
        static event Action SpecUpdated;

        internal static void UpdateSpec(Dictionary<string,object> vals)
        {
            spec = vals;
            SpecUpdated?.Invoke();
        }

        /// <summary>
        /// Use if you want to set multiple at once
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [LunarBindDocumentation("Sets a value without pushing, useful if you want to set multiple values at a time")]
        [LunarBindFunction]
        public static void SetWithoutPush(string key, object value)
        {
            spec[key] = value;
        }

        [LunarBindDocumentation("Sets a value and pushes it to the other process")]
        [LunarBindFunction]
        public static void Set(string key, object value)
        {
            spec[key] = value;
            Push();
        }

        [LunarBindDocumentation("Pushes the Spec to the other process")]
        [LunarBindFunction]
        public static void Push()
        {
            if (PluginCore.CurrentSide == RTCSide.Client)
            {
                //Send to UI
                LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.UPDATE_SPEC, spec, true);
            }
            else
            {
                //Send to Emulator
                LocalNetCoreRouter.Route(Routing.Endpoints.EMU_SIDE, Routing.Commands.UPDATE_SPEC, spec, true);
            }
        }

        public static T Get<T>(string name)
        {
            if(spec.TryGetValue(name, out object obj))
            {
                return (T)obj;
            }
            else
            {
                return default(T);
            }
        }
        [LunarBindDocumentation("Gets a value from the Spec")]
        [LunarBindFunction]
        public static object Get(string name)
        {
            if (spec.TryGetValue(name, out object obj))
            {
                return obj;
            }
            else
            {
                return null;
            }
        }

    }
}
