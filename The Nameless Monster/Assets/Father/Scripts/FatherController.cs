namespace TheNamelessMonster.Father 
{
	using UnityEngine;

	public class FatherController : MonoBehaviour 
	{
		public FatherBll Bll;

        // Only update per entity
        void Update()
        {
            Bll.CheckGround();
        }
    }
}
