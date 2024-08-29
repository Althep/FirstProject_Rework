using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicInventory
{
    List<MagicBase> _playerMagics= new List<MagicBase>();
    
    public List<MagicBase> playerMagics { get { return _playerMagics; } }
   
}
