namespace TheNamelessMonster.Portal 
{
	using UnityEngine;

	public class PortalController : MonoBehaviour 
	{

		public PortalBll Bll;
		
		// Only update per entity
		void Update()
		{
            if (Input.GetKeyDown(KeyCode.U))
            {
                //Bll.AnimateSmallToLarge();
            }
		}
	}
}
