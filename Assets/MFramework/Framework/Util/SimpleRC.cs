namespace MFramework
{
    public interface IRefCounter
    {
        int RefCount { get; }

        void Retain(object refOwner = null);

        void Release(object refOwner = null);
    }
    public class SimpleRC : IRefCounter
    {
        public int RefCount { get; private set; }

        public void Release(object refOwner = null)
        {
            RefCount--;
            if(RefCount == 0)
            {
                OnZeroRef();
            }
        }

        public void Retain(object refOwner = null)
        {
            RefCount++;
        }

        protected virtual void OnZeroRef()
        {

        }
    }

}
