
namespace MgEngine.Interface
{
    internal interface IObj : IBox2D
    {
        public void Load();
        public void Update();
        public void Draw();
        public void Unload();
    }
}
