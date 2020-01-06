using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    int healt = 3;
    public Image[] hearts;
    bool hasCooldown = false;

    public SceneChange sceneChange;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GetComponent<PlayerMovement>().isGrounded)
            {
                SubstractHealt();
            }
        }
    }

    void SubstractHealt()
    {
        if (!hasCooldown)
        {
            if (healt > 0) {
                healt--;
                hasCooldown = true;
                StartCoroutine(CoolDown());
            }

            if (healt <= 0)
            {
                sceneChange.ChangeSceneTo("LoseScene");
            }

            EmptyHearts();
        }
    }

    void EmptyHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (healt - 1 < i)
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(.5f);
        hasCooldown = false;

        StopCoroutine(CoolDown());
    }
}
