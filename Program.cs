using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        Hero hero = new Hero("Harley", 1000, 500, 20);
        Musuh musuh = new Musuh("Kadita", 1000, 500, 10);

        GameRPG.Characters.Add(hero);
        GameRPG.Characters.Add(musuh);

        Heal heal = new Heal();
        IceBlast iceBlast = new IceBlast();
        Fireball fireball = new Fireball();

        hero.cetakInformasi();
        musuh.cetakInformasi();

        hero.gunakanSkill(fireball);
        musuh.cetakInformasi();
    }
}

public static class GameRPG
{
    public static List<Karakter> Characters = new List<Karakter>();
}

public abstract class Karakter
{
    public string Nama;
    public int HP, MP, Kekuatan;

    public Karakter(string Nama, int HP, int MP, int Kekuatan)
    {
        this.Nama = Nama;
        this.HP = HP;
        this.MP = MP;
        this.Kekuatan = Kekuatan;
    }

    public abstract void gunakanSkill(ISkill skill);
    public void cetakInformasi()
    {
        Console.WriteLine($"==Info==" +
            $"\nNama: {Nama}\nHP: {HP}\nMP: {MP}\nKekuatan: {Kekuatan}\n");
    }
}

public interface ISkill
{
    void Aktif(Karakter karakter, Karakter musuh);
}

public class Heal : ISkill
{
    public void Aktif(Karakter karakter, Karakter musuh)
    {
        karakter.HP += 20;
        Console.WriteLine($"{karakter.Nama} memulihkan HP sebesar 20");
    }
}

public class Fireball : ISkill
{
    public void Aktif(Karakter karakter, Karakter musuh)
    {
        int totalDamageHero = 30 * karakter.Kekuatan, totalDamageMusuh = 30 * musuh.Kekuatan;

        if (karakter is Hero)
        {
            musuh.HP -= totalDamageHero;
            Console.WriteLine($"{karakter.Nama} menyerang {musuh.Nama} dengan skill Fireball memberikan damage sebesar {totalDamageHero}");
        }
        else if (karakter is Musuh)
        {
            musuh.HP -= totalDamageMusuh;
            Console.WriteLine($"{karakter.Nama} menyerang {musuh.Nama} dengan skill Fireball memberikan damage sebesar {totalDamageMusuh}");
        }
    }
}

public class IceBlast : ISkill
{
    public void Aktif(Karakter karakter, Karakter musuh)
    {
        int totalDamageHero = 15 * karakter.Kekuatan, totalDamageMusuh = 15 * musuh.Kekuatan;
        if (karakter is Hero)
        {
            musuh.HP -= totalDamageHero;
            Console.WriteLine($"{karakter.Nama} menyerang {musuh.Nama} dengan skill IceBlast, memberikan damage {totalDamageHero} dan memperlambat gerakan");
        }
        else if (karakter is Musuh)
        {
            musuh.HP -= 15 * totalDamageMusuh;
            Console.WriteLine($"{karakter.Nama} menyerang {musuh.Nama} dengan skill IceBlast, memberikan damage {totalDamageMusuh} dan memperlambat gerakan");
        }
    }
}

public class Hero : Karakter
{
    public Hero(string Nama, int HP, int MP, int Kekuatan) : base(Nama, HP, MP, Kekuatan)
    {
    }

    public void serang(Karakter musuh)
    {
        musuh.HP -= Kekuatan;
        Console.WriteLine($"{Nama} menyerang {musuh.Nama}, HP {musuh.Nama} berkurang {Kekuatan}");
    }

    public override void gunakanSkill(ISkill skill)
    {
        if (MP >= 20)
        {
            foreach (var karakter in GameRPG.Characters)
            {
                if (karakter is Musuh)
                {
                    skill.Aktif(this, karakter);
                    MP -= 20;
                    return;
                }
            }
        }
        else
        {
            Console.WriteLine($"{Nama} tidak memiliki MP yang cukup untuk menggunakan Skill");
        }
    }
}

public class Musuh : Karakter
{
    public Musuh(string Nama, int HP, int MP, int Kekuatan) : base(Nama, HP, MP, Kekuatan)
    {
    }

    public void serang(Hero hero)
    {
        hero.HP -= Kekuatan;
        Console.WriteLine($"{Nama} menyerang {hero.Nama}, HP {hero.Nama} berkurang {Kekuatan}");
    }

    public override void gunakanSkill(ISkill skill)
    {
        if (MP >= 20)
        {
            foreach (var karakter in GameRPG.Characters)
            {
                if (karakter is Hero && karakter != this)
                {
                    skill.Aktif(this, karakter);
                    MP -= 20;
                    return;
                }
            }
        }
        else
        {
            Console.WriteLine($"{Nama} tidak memiliki MP yang cukup untuk menggunakan Skill");
        }
    }

}
