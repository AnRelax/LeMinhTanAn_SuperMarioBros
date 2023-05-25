using UnityEngine;
using System.Collections;
using TMPro;

public class Damageable : MonoBehaviour
{
    public static Damageable Instance { get; set; }
    public GameObject damageTextPrefab;
    public void TakeDamage(int damage, bool isCritical)
    {
        GameObject damageTextObject = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        TextMeshPro damageTextMesh = damageTextObject.GetComponent<TextMeshPro>();
        if(damageTextMesh != null){
            damageTextMesh.text = damage.ToString("N0");
            if (isCritical){
                damageTextMesh.color = Color.red;
            }
            StartCoroutine(FlyUpFadeOutAndDestroy(damageTextObject, 2f, 1.25f));
        }else{
            Debug.Log("khong tim thay text mesh");
        }
    }
    private IEnumerator FlyUpFadeOutAndDestroy(GameObject obj, float duration, float flySpeed)
    {
        TextMeshPro textMesh = obj.GetComponent<TextMeshPro>();
        if (textMesh != null){
            float elapsedTime = 0f;
            float initialYPosition = obj.transform.position.y + 0.4f;
            Color startColor = textMesh.color;
            while (elapsedTime < duration){
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

                float newYPosition = initialYPosition + (flySpeed * elapsedTime);
                obj.transform.position = new Vector3(obj.transform.position.x, newYPosition, obj.transform.position.z);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Destroy(obj);
        }
    }
}
