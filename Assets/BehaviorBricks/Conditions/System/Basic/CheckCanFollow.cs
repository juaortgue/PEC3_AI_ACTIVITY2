using UnityEngine;
using Pada1.BBCore.Framework;
using Pada1.BBCore;

namespace BBCore.Conditions
{
    /// <summary>
    /// It is a basic condition to check if Booleans have the same value.
    /// </summary>
    [Condition("Basic/CheckCanFollow")]
    [Help("Checks if Police can follow thief")]
    public class CheckCanFollow : ConditionBase
    {
        ///<value>Input First Boolean Parameter.</value>
        [InParam("PoliceMan")]
        [Help("PoliceMan game object")]
        public GameObject policeMan;

       

		public override bool Check()
		{
			PoliceManagement tgt = policeMan.GetComponent<PoliceManagement>(); 
            return tgt.canFollow;
		}
    }
}