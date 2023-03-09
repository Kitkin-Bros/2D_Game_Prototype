using UnityEngine;
using Zenject;

namespace Services.Inject {
    public abstract class BaseMonoInstaller : MonoInstaller {
        protected void BindScriptableObject<T>(T parameter) where T : ScriptableObject{
            Container.Bind<T>().FromScriptableObject(parameter).AsSingle();
        }
    
        protected void BindObjectFromInstance<T>(T parameter) where T : MonoBehaviour{
            Container.Bind<T>().FromInstance(parameter).AsSingle();
        }
    
        protected void BindNonMonoBehaviourClassAsSingle<T>() {
            Container.Bind<T>().AsSingle();
        }

        protected void BindObjectFromHierarchy<T>() {
            Container.Bind<T>().FromComponentInHierarchy().AsSingle();
        }
    }
}