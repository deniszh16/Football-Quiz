using Firebase.Analytics;

namespace Code.Services.Analytics
{
    public class FirebaseService : IFirebaseService
    {
        private Firebase.FirebaseApp _app;
        
        public void Initialization()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    _app = Firebase.FirebaseApp.DefaultInstance;
                } else {
                    UnityEngine.Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }

        public void SubmitAnEvent(string id) =>
            FirebaseAnalytics.LogEvent(id);

        public void SubmitAnEvent(string id, Parameter parameter) =>
            FirebaseAnalytics.LogEvent(id, parameter);
    }
}