using UnityEngine;

public class KeyPressHandler : MonoBehaviour
{
    public KeyCode targetKey = KeyCode.Space; // Phím bạn muốn kiểm tra
    private float holdTime = 0f; // Thời gian giữ phím

    void Update()
    {
        // Kiểm tra khi nhấn phím một lần
        if (Input.GetKeyDown(targetKey))
        {
            Debug.Log("Key Pressed Once!");
        }

        // Kiểm tra khi phím đang được giữ
        if (Input.GetKey(targetKey))
        {
            holdTime += Time.deltaTime; // Cộng dồn thời gian giữ phím
            Debug.Log($"Key Held for {holdTime:F2} seconds");
        }

        // Kiểm tra khi thả phím
        if (Input.GetKeyUp(targetKey))
        {
            Debug.Log($"Key Released after being held for {holdTime:F2} seconds");
            holdTime = 0f; // Reset thời gian giữ phím
        }
    }
}
