using UnityEngine;
using System.Collections;

public class Fight2D : MonoBehaviour
{



	public static bool playerGodMod = false;
	public static bool  isEnemyNear=false;
	// функция возвращает ближайший объект из массива, относительно указанной позиции
	static GameObject NearTarget(Vector3 position, Collider2D[] array)
	{
		Collider2D current = null;
		float dist = Mathf.Infinity;

		foreach (Collider2D coll in array)
		{
			float curDist = Vector3.Distance(position, coll.transform.position);

			if (curDist < dist)
			{
				current = coll;
				dist = curDist;
			}
		}

		return (current != null) ? current.gameObject : null;
	}

	// point - точка контакта
	// radius - радиус поражения
	// layerMask - номер слоя, с которым будет взаимодействие
	// damage - наносимый урон
	// allTargets - должны-ли получить урон все цели, попавшие в зону поражения
	public static void Action(Vector2 point, float radius, int layerMask, int damage, bool allTargets)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);

		if (!allTargets)
		{
			GameObject obj = NearTarget(point, colliders);
			if (obj != null)
			{
				
				isEnemyNear = true;
				if (layerMask == 10 && obj.name=="Enemy")
				{
					Enemy enemy = obj.GetComponent<Enemy>();
					enemy.currentHealth -= damage;
					Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
					rb.velocity = Vector3.zero;
					rb.AddForce(enemy.transform.up * 3.0F, ForceMode2D.Impulse);

				}
				if (layerMask == 10 && obj.name.StartsWith("Wolf"))
				{
					WolfScript enemy = obj.GetComponent<WolfScript>();
					enemy.currentHealth -= damage;
					Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
					rb.velocity = Vector3.zero;
					rb.AddForce(enemy.transform.up * 3.0F, ForceMode2D.Impulse);

				}
				if (layerMask == 10 && obj.name.StartsWith("Skeleton"))
				{
					SkeletonScript enemy = obj.GetComponent<SkeletonScript>();
					enemy.currentHealth -= damage;
					Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
					rb.velocity = Vector3.zero;
					rb.AddForce(enemy.transform.up * 3.0F, ForceMode2D.Impulse);

				}
				if (layerMask == 10 && obj.name.StartsWith("Boss"))
				{
					BossScript enemy = obj.GetComponent<BossScript>();
					enemy.currentHealth -= damage;
					Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
					rb.velocity = Vector3.zero;
					rb.AddForce(enemy.transform.up * 3.0F, ForceMode2D.Impulse);

				}
				if (layerMask == 11)
				{
					PlayerController p = obj.GetComponent<PlayerController>();
					if (!p.godMod)
					{
						p.currentHealth -= 5;
						p.SetGodMod();
					}

				}
			}
			else
				isEnemyNear = false;		}
	}

	

}