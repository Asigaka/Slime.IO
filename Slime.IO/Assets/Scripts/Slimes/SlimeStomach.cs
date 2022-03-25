using UnityEngine;

public class SlimeStomach : MonoBehaviour
{
    [SerializeField] private float sizeMultiplier = 0.01f;
    [SerializeField] private float slimeMaxSize = 200;
    [SerializeField] private float slimeSize;
    [SerializeField] private float startStomachFullness = 100;
    [SerializeField] private Transform slimeBody;

    [SerializeField] private float stomachFullness;

    public float SlimeSize { get => slimeSize; }
    public float StomachFullness { get => stomachFullness; }

    private void Start()
    {
        stomachFullness = startStomachFullness;
        UpdateSize();
    }

    public void IncreaseStomachFullness(float foodValue)
    {
        stomachFullness += foodValue;
        UpdateSize();
    }

    public void ResetFullness()
    {
        stomachFullness = startStomachFullness;
        UpdateSize();
    }

    public void UpdateSize()
    {
        slimeSize = stomachFullness * sizeMultiplier;

        if (slimeSize > slimeMaxSize)
        {
            slimeSize = slimeMaxSize;
        }

        slimeBody.localScale = new Vector3(slimeSize, slimeSize, slimeSize);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckGameObject(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckGameObject(other.gameObject);
    }

    private void CheckGameObject(GameObject checkedObject)
    {
        //Debug.Log(checkedObject.name + " тронул " + checkedObject.name);
        FoodObject food = checkedObject.GetComponent<FoodObject>();
        SlimeStomach slime = null;

        if (checkedObject.tag == "Slime")
            slime = checkedObject.GetComponentInChildren<SlimeStomach>();

        if (food)
        {
            if (StomachFullness >= food.Info.StomachFullness)
            {
                //Debug.Log(checkedObject.name + " съел " + food.gameObject.name);
                IncreaseStomachFullness(food.Info.FoodValue);
                MapGeneration.Instance.DestroyObject(food.gameObject);
                SlimesManager.Instance.UpdateLeaderboard();
            }
        }
        else if (slime)
        {
            if (StomachFullness >= slime.StomachFullness)
            {
                IncreaseStomachFullness(slime.StomachFullness / 2);
                SlimesManager.Instance.RespawnSlime(slime);
                SlimesManager.Instance.UpdateLeaderboard();
            }
        }
    }
}
