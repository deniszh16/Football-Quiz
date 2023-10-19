using Firebase.Analytics;

namespace Services.Analytics
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
                    UnityEngine.Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }

        public void SubmitAnEvent(string id) =>
            FirebaseAnalytics.LogEvent(id);

        public void SubmitAnEvent(string id, (string, int) parameter)
        {
            var firebaseParameter = new Parameter(parameter.Item1, parameter.Item2);
            SubmitAnEventWithParameter(id, firebaseParameter);
        }

        private void SubmitAnEventWithParameter(string id, Parameter parameter) =>
            FirebaseAnalytics.LogEvent(id, parameter);
    }
}