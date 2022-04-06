#define FH_DEBUG_LOG 
//#define FH_RELEASE_LOG

using UnityEngine;
using System.Collections;

namespace FH
{
    public static class FHLog
    {
        public static void Log(object message)
        {
#if (DEBUG && FH_DEBUG_LOG) || (FH_RELEASE_LOG)
            Debug.Log("FHLog: " + message);
#endif
        }

        public static void Log(object message, Object context)
        {
#if (DEBUG && FH_DEBUG_LOG) || (FH_RELEASE_LOG)
            Debug.Log("FHLog: " + message, context);
#endif
        }

        public static void LogWarning(object message)
        {
#if (DEBUG && FH_DEBUG_LOG) || (FH_RELEASE_LOG)
            Debug.LogWarning("FHLog: " + message);
#endif
        }

        public static void LogWarning(object message, Object context)
        {
#if (DEBUG && FH_DEBUG_LOG) || (FH_RELEASE_LOG)
            Debug.LogWarning("FHLog: " + message, context);
#endif
        }

        public static void LogError(object message)
        {
#if (DEBUG && FH_DEBUG_LOG) || (FH_RELEASE_LOG)
            Debug.LogError("FHLog: " + message);
#endif
        }

        public static void LogError(object message, Object context)
        {
#if (DEBUG && FH_DEBUG_LOG) || (FH_RELEASE_LOG)
            Debug.LogError("FHLog: " + message, context);
#endif
        }
    }

}