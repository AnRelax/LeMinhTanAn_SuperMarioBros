using System.Collections;
using UnityEngine;

public class SpamDestroyBlock : MonoBehaviour
{
    public GameObject blockPrefab;
    public float jumpForce = 5f;
    public float fallForce = 50f;
    public float spawnDelay = 0.0001f;
    public int numberOfBlocks = 6;
    public Transform spawnPoint;

    public Vector2[] jumpDirections = {new Vector2(0f, 1f), new Vector2(0.3f, 1f), new Vector2(0.6f, 1f), new Vector2(-0.3f, 1f), new Vector2(-0.6f, 1f)};

    public void SpamBlock(){
        StartCoroutine(SpawnBlocks());
    }

    private IEnumerator SpawnBlocks()
    {
        for (int i = 0; i < numberOfBlocks; i++)
        {
            GameObject block = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D blockRigidbody = block.GetComponent<Rigidbody2D>();

            // Lựa chọn ngẫu nhiên hướng bay
            Vector2 randomJumpDirection = jumpDirections[Random.Range(0, jumpDirections.Length)];

            // Đẩy game object lên với hướng bay ngẫu nhiên
            blockRigidbody.AddForce(randomJumpDirection * jumpForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(spawnDelay);
            Destroy(block, 2.5f);
        }
    }
}
