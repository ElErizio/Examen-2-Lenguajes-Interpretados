using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance = default;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject();
                    gameObject.name = typeof(T).Name;
                    gameObject.hideFlags = HideFlags.HideAndDontSave;
                    _instance = gameObject.AddComponent<T>();
                }
                return _instance;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
}