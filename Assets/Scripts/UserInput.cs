using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    // Start is called before the first frame update

    public float minCameraVertical = -1f;
    public float maxCameraVertical = 1f;
    public float minCameraHorizontal = -1f;
    public float maxCameraHorizontal = 1f;

    public Camera GameCamera;
    public float PanSpeed = 10.0f; 

    private bool SelectEnemy(out Enemy e)
    {
        e = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit t))
        {
            var enemy = t.collider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {

                e = enemy;
                return true;

            }
        }
        return false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            GameCamera.transform.position = GameCamera.transform.position + PanSpeed * Time.deltaTime * new Vector3(move.y, 0, -move.x);
            Transform t = GameCamera.transform;
            Vector2 adjust;
            adjust.y = Mathf.Clamp(t.position.x, minCameraHorizontal, maxCameraHorizontal);
            adjust.x = Mathf.Clamp(t.position.z, minCameraVertical, maxCameraVertical);

            GameCamera.transform.position = new Vector3(adjust.y, t.position.y, adjust.x);

            if (Input.GetMouseButtonDown(0))
            {
                if (SelectEnemy(out Enemy enemy))
                    if (enemy.Health > 0)
                        enemy.TakeDamage(1);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (SelectEnemy(out Enemy enemy))
                    if (enemy.Health > 0)
                        enemy.SetText();
            } 
        }
    }
}
