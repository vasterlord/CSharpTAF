using Support.Utils;
using System.Threading;

namespace Support.LocalConfigs
{
    public class LocalConfigsEntityContainer
    {
        private static ThreadLocal<LocalConfigsEntity> _localConfigsEntity = new ThreadLocal<LocalConfigsEntity>();

        public const string LOCAL_CONFIGS_FILE_NAME = "localConfigs.json";


        public static LocalConfigsEntity GetLocalConfigs()
        {
            if (_localConfigsEntity.Value == null)
            {
                _localConfigsEntity.Value = SearializerUtils<LocalConfigsEntity>.ReadJsonObject(LOCAL_CONFIGS_FILE_NAME);
            }

            return _localConfigsEntity.Value;
        }
    }
}