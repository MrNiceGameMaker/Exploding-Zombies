using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpecialPowerEngine : MonoBehaviour
{
    float speed;
    bool moveDown;
    bool playPS = false;
    public int listID;
    [SerializeField] GameObject particalSystemExp;

    private void OnEnable()
    {
        speed = 100;
        StartCoroutine(IncreaseCoinSpeed());
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,speed*Time.deltaTime,0);
        if (moveDown) 
            transform.position = new Vector3(transform.position.x, transform.position.y - 1f*Time.deltaTime, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f * Time.deltaTime, transform.position.z);
    }

    IEnumerator IncreaseCoinSpeed()
    {
        for(int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.2f);
            speed += 80;
            if (transform.position.y > 1.75f) moveDown = true; 
            else if (transform.position.y < 1.25f) moveDown = false;
        }
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (playPS)
        {
            GameObject temp = Instantiate(particalSystemExp);
            temp.transform.position = transform.position;
            Destroy(temp, 2);
        }else
        {
            playPS = true;
        }

    }
}
