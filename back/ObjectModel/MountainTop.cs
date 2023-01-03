namespace back.ObjectModel
{
    public class MountainTop
    {
        public virtual long Id { get; set;}
        public virtual string Name { get; set;}
        public virtual int Height { get; set; }
        public virtual Mountain MountainName { get; set; }
        public MountainTop() {}
    }
}