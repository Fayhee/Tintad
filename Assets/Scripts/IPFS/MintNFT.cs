using System.Collections;
using System.Collections.Generic;
using Models;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class MintNFT : MonoBehaviour
{

    // Start is called before the first frame update
    public string chain = "ethereum";
    public string network = "rinkeby"; // mainnet ropsten kovan rinkeby goerli
    public string account = "0xcdAd1bA42d996581e6e83158Fd13b3eEF20BaCD0";
    public GetAccount getAccount;
    public string to;
    public TMP_Text theCID;
    public string cid;

    async public void MintButtonPK()
    {

        getAccount = GetComponent<GetAccount>();
        to = getAccount.GetTheAccount();
        cid = theCID.text;
        CreateMintModel.Response nftResponse = await EVM.CreateMint(chain, network, account, to, cid);
        if (nftResponse != null)
        {
            Debug.Log("CID: " + nftResponse.cid);
            Debug.Log("Connection: " + nftResponse.connection);
            Debug.Log("TC Account: " + nftResponse.tx.account);
            Debug.Log("TX Data: " + nftResponse.tx.data);
            Debug.Log("TX Value: " + nftResponse.tx.value);
            Debug.Log("TX Gas Limit: " + nftResponse.tx.gasLimit);
            Debug.Log("TX Gas Price: " + nftResponse.tx.gasPrice);
            Debug.Log("Hashed Unsigned TX: " + nftResponse.hashedUnsignedTx);
            string chainId = await EVM.ChainId(chain, network, "");
            Debug.Log("Chain Id: " + chainId);
            string gasPrice1 = await EVM.GasPrice(chain, network, "");
            Debug.Log("Gas Price: " + gasPrice1);

            // private key of account
            string privateKey = "71d28503b282cb610b9d1714f86cc01e802999c8e81411f6f9fa7ca2c2f8afb9";
            Debug.Log("Account: " + account);
            string transaction = await EVM.CreateTransaction(chain, network, nftResponse.tx.account,
                nftResponse.tx.to, nftResponse.tx.value, nftResponse.tx.data,
                nftResponse.tx.gasPrice, nftResponse.tx.gasLimit);
            Debug.Log("Transaction: " + transaction);
            string signature = Web3PrivateKey.SignTransaction(privateKey, transaction, chainId);
            Debug.Log("Signature: " + signature);
            //string rpc = "";
            string responseBroadcast = await EVM.BroadcastTransaction(chain, network, nftResponse.tx.account,
                nftResponse.tx.to, nftResponse.tx.value, nftResponse.tx.data, signature,
                nftResponse.tx.gasPrice, nftResponse.tx.gasLimit, "");
            Debug.Log("Response: " + responseBroadcast);
        }
    }
}