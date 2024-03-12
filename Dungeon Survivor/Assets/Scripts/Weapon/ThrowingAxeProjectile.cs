using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeProjectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    public int dmg = 5;
    float timeToDelete = 2f;

    public void setDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y);
        if (dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }
    bool hitDetected = false;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.7f);
            foreach (Collider2D c in hit)
            {
                IDamageable damageable = c.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    PostDamage(dmg, transform.position);
                    damageable.TakeDamage(dmg);
                    hitDetected = true;
                    break;
                }
            }
            if (hitDetected == true)
            {
                Destroy(gameObject);
            }

            timeToDelete -= Time.deltaTime;
            if( timeToDelete < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void PostDamage(int damage, Vector3 worldPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition);
    }
}
