using PimDeWitte.UnityMainThreadDispatcher;
using DZGames.Football.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DZGames.Football.LifetimeScopes
{
    public class GlobalLifetimeScope : LifetimeScope
    {
        [SerializeField] private SceneLoaderService _sceneLoaderService;
        [SerializeField] private UnityMainThreadDispatcher _unityMainThreadDispatcher;
        
        protected override void Configure(IContainerBuilder builder)
        {
            BindStaticData(builder);
            BindPersistentProgress(builder);
            BindSceneLoader(builder);
            BindSaveLoadService(builder);
            BindAdService(builder);
            BindGooglePlayService(builder);
            BindFirebaseService(builder);
            BindMainThreadDispatcher(builder);
            DontDestroyOnLoad(this);
        }
        
        private void BindStaticData(IContainerBuilder builder)
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadQuestionsCountries();
            staticDataService.LoadQuestionsFacts();
            staticDataService.LoadQuestionsPlayers();
            staticDataService.LoadLegends();
            builder.RegisterInstance(staticDataService);
        }
        
        private void BindPersistentProgress(IContainerBuilder builder) =>
            builder.Register<PersistentProgressService>(Lifetime.Singleton).AsImplementedInterfaces();
        
        private void BindSceneLoader(IContainerBuilder builder) =>
            builder.RegisterComponent(_sceneLoaderService).As<ISceneLoaderService>();
        
        private void BindSaveLoadService(IContainerBuilder builder) =>
            builder.Register<SaveLoadService>(Lifetime.Singleton).AsImplementedInterfaces();
        
        private void BindAdService(IContainerBuilder builder)
        {
            IAdService adService = new AdService();
            adService.Initialization();
            builder.RegisterInstance(adService);
        }
        
        private void BindGooglePlayService(IContainerBuilder builder)
        {
            IGooglePlayService googlePlayService = new GooglePlayService();
            googlePlayService.ActivateService();
            builder.RegisterInstance(googlePlayService);
        }

        private void BindFirebaseService(IContainerBuilder builder)
        {
            IFirebaseService firebaseService = new FirebaseService();
            firebaseService.Initialization();
            builder.RegisterInstance(firebaseService);
        }

        private void BindMainThreadDispatcher(IContainerBuilder builder) =>
            builder.RegisterComponent(_unityMainThreadDispatcher);
    }
}