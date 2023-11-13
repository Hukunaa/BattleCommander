using UnityEngine;

//JSON FILE CONTAINING ALL TYPES OF UNITS NEW IDEA!!!!

[System.Serializable]
public class UnitSettings
{
    public enum ShapeType { CUBE, SPHERE }
    public enum SizeType { SMALL, BIG }
    public enum ColorType { BLUE, GREEN, RED }

    private int _health;
    private int _attack;
    private int _attackSpeed;
    private int _speed;
    private float _attackRange;

    //_targetMode: 0 = closest enemy / 1 = weakest enemy alive
    private int _targetMode;

    private ShapeType _shape;
    private SizeType _size;
    private ColorType _color;
    public UnitSettings(ShapeType shape, SizeType size, ColorType color)
    {
        _health = 100;
        _attack = 10;
        _speed = 10;
        _attackRange = 2.5f;
        _attackSpeed = 1;
        _shape = shape;
        _size = size;
        _color = color;

        switch (shape)
        {
            case ShapeType.CUBE:
                _health += 100;
                _attack += 10;
                _targetMode = 0;
                break;

            case ShapeType.SPHERE:
                _health += 50;
                _attack += 20;
                _targetMode = 1;
                break;
        }

        switch (size)
        {
            case SizeType.SMALL:
                _health -= 50;
                break;

            case SizeType.BIG:
                _health += 50;
                break;
        }

        switch(color)
        {
            case ColorType.BLUE:
                _attack -= 15;
                _attackSpeed += 4;
                _speed += 10;
                break;

            case ColorType.GREEN:
                _health -= 100;
                _attack += 20;
                _speed -= 5;
                break;

            case ColorType.RED:
                _health += 200;
                _attack += 40;
                _speed -= 9;
                break;
        }
    }

    public int Health { get => _health; }
    public int Attack { get => _attack; }
    public int AttackSpeed { get => _attackSpeed; }
	public float AttackRange { get => _attackRange; }
    public int Speed { get => _speed; }
    public int TargetMode { get => _targetMode;}
    public ShapeType Shape { get => _shape; }
    public SizeType Size { get => _size; }
    public ColorType Color { get => _color; }
}
