namespace TheNamelessMonster.SmallChild 
{
	using UnityEngine;

	public class SmallChildController : MonoBehaviour 
	{
		public SmallChildBll Bll;
		
		// Only update per entity
		void Update()
		{
            Bll.CheckGround();
        }
    }
}
