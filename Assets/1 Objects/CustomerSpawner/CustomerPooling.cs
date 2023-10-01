using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPooling : MonoBehaviour
{
    private List<CustomerBrain> customerPool;
    [SerializeField] private int basePoolSize;
    [SerializeField] private CustomerBrain customerPrefab;

    private void Awake()
    {
        customerPool = new List<CustomerBrain>();

        for (int i = 0; i < basePoolSize; i++) {
            CustomerBrain newCustomer = Instantiate(customerPrefab, transform);
            customerPool.Add(newCustomer);
            newCustomer.gameObject.SetActive(false);
        }
    }

    public CustomerBrain GetCustomer()
    {
        if(customerPool.Count == 0)
        {
            return Instantiate(customerPrefab);
        }
        CustomerBrain customer = customerPool[0];
        customerPool.RemoveAt(0);
        customer.gameObject.SetActive(true);
        return customer;
    }

    public void ReturnCustomer(CustomerBrain customer)
    {
        customerPool.Add(customer);
        customer.gameObject.SetActive(false);
    }
}
