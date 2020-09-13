namespace SaveData {

    [System.Serializable]
    public class EnvironmentData {

        public object[][] environment;

        public EnvironmentData() {
            environment = new object[25000][];
        }

        /*    OBJECT ARRAY TRANSLATOR:
         *    
         *    IN A SYSTEM OBJECT: { OBJECT TYPE }
         *    IN A STAR OBJECT: { OBJECT TYPE, PREFAB INDEX }
         *    IN A PLANET OBJECT: { OBJECT TYPE, PREFAB INDEX, DISTANCE}
         * 
         */
    }
}