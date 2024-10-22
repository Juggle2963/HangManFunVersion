using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scratch2;
internal class PlayerInfo
{
    public double TotalTries { get; set; }
    public double NumbOfCorrectGuesses { get; set; }

    private string? _name;

	public string Name
	{
		get { return _name!; }
		set 
		{
			if (value.Length < 2)
				value = "unknown";
			_name = value; 
		}
	}
    public PlayerInfo(string Name)
    {
        this.Name = Name;
    }



}
