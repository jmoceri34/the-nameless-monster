namespace TheNamelessMonster.Monster 
{
	using UnityEngine;

	public class MonsterController : MonoBehaviour 
	{
		public MonsterBll Bll;
		
		// Only update per entity
		void Update()
		{
            Bll.CheckGround();
        }
    }
}
