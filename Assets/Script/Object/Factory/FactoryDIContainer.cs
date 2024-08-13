public class FactoryDIContainer
{
    public ItemFactory itemFac
    {
        get;
        private set;
    }
    //public MonsterFactory monFac;
    public PlayerFactory playerFac
    {
        get;
        private set;
    }
    public ActionFactory actionFac
    {
        get;
        private set;
    }
    public HitBoxFactory hitBoxFac
    {
        get;
        private set;
    }
    public AffectFactory affectFac
    {
        get;
        private set;
    }

    public FactoryDIContainer() 
    {
        affectFac = new AffectFactory();
        hitBoxFac = new HitBoxFactory(affectFac);
        actionFac = new ActionFactory(hitBoxFac);
        itemFac = new ItemFactory(actionFac);
    }

}
