namespace MessageBird.Resources
{
    public class GroupLists : BaseLists<Objects.Group>
    {
        public GroupLists()
            : base("groups", new Objects.GroupList())
        {
            //
        }
    }
}
