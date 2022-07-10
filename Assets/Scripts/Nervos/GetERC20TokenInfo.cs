using System.Numerics;
using UnityEngine;
using TMPro;

public class GetERC20TokenInfo : MonoBehaviour
{
    // Chain == Chain name connecting
    [SerializeField] private string chain = "godwoken";

    // Network == The network RPC name
    [SerializeField] private string network = "testnet-v1";

    // USDC address == Token Contract Address
    [SerializeField] private string contract = "0xD233FFD436D68235B5fB51dfB9e368b598Dc5F9B";

    // rpc of chain
    [SerializeField] private string rpc = "https://godwoken-testnet-v1.ckbapp.dev";

    public TMP_Text tokenName;
    public TMP_Text tokenName2;

    async void Start()
    {
        BigInteger totalSupply = await ERC20.TotalSupply(chain, network, contract, rpc);
        Debug.Log(totalSupply);

        string symbol = await ERC20.Symbol(chain, network, contract, rpc);
        Debug.Log(symbol);

        string name = await ERC20.Name(chain, network, contract, rpc);
        tokenName.text = name;
        tokenName2.text = name;
        Debug.Log(name);


        

        

    }
}
