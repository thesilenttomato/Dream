public class BaseUnitData
{
    public float life;
    public float attack;
    public float attackInterval;
    public float movementSpeed;
    public int bulletSpeed;

    public BaseUnitData(float life, float attack, float attackInterval, float movementSpeed, int bulletSpeed)
    {
        this.life = life;
        this.attack = attack;
        this.attackInterval = attackInterval;
        this.movementSpeed = movementSpeed;
        this.bulletSpeed = bulletSpeed;

    }
}
