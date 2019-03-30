using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    #region Private & Static Variables
    private double HealthMax;
    #endregion

    #region Public Variables
    public double Health = 50;
    public Transform HealthBar;
    #endregion

    #region Public Methods
    public void Effect(double var)
    {
        Health += var;
        Health = Health > HealthMax ? HealthMax : Health;

        // Play hit animation of parent

        if (HealthBar != null) {
            HealthBar.transform.localScale = new Vector3((float)(Health / HealthMax),1);
        }
        else{
            Debug.LogError(gameObject.name + " :HealthBar is null.");
        }
        

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Private Methods
    private void Awake()
    {
        this.HealthMax = this.Health;
        this.HealthBar = HealthBar== null ? transform.Find("HealthBar")?.Find("BarContainer")?.transform : HealthBar;
    }
    #endregion
}
