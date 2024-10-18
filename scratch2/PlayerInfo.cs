using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scratch2;
internal class PlayerInfo
{
    public double totalTries { get; set; }
    public double numbOfCorrectGuesses { get; set; }

    private string _name;

	public string Name
	{
		get { return _name; }
		set 
		{
			if (value.Length < 2)
				throw new ArgumentException("player name cant be less then two character");
			_name = value; 
		}
	}
    public PlayerInfo(string Name)
    {
        this.Name = Name;
    }



}
