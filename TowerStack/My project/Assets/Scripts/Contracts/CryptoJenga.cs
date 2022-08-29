using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_WEBGL
public class CryptoJenga: MonoBehaviour
{
    [SerializeField]
    InputField ABIInputField;

    [SerializeField]
    InputField ContractAddressField;

    public static string gameFactoryAbi = "[ { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"_USDTicketPrice\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_roundDuration\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_totalRounds\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_maxBets\", \"type\": \"uint256\" }, { \"internalType\": \"string\", \"name\": \"_gameCode\", \"type\": \"string\" } ], \"name\": \"createGame\", \"outputs\": [ { \"internalType\": \"contract cryptoJengaV6\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"name\": \"games\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"_gameCode\", \"type\": \"string\" } ], \"name\": \"getGameAddress\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"_gameCode\", \"type\": \"string\" } ], \"name\": \"removeGame\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";
    private static string gameFactoryAddress = "0xEb58f4ABfF20aeE0b2a1C33e4d8ee8b09c0a73AD";

    public static string gameAbi = "[ { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"_USDTicketPrice\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_roundDuration\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_totalRounds\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_maxBets\", \"type\": \"uint256\" }, { \"internalType\": \"string\", \"name\": \"_gameCode\", \"type\": \"string\" }, { \"internalType\": \"contract GameFactoryInterface\", \"name\": \"gameFactoryAddress\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"have\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"want\", \"type\": \"address\" } ], \"name\": \"OnlyCoordinatorCanFulfill\", \"type\": \"error\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"_player\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_currentRoundBetCount\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_remainingBets\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"BetMade\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"_gameWinner\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"amountWon\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"GameEnded\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"string\", \"name\": \"_currentState\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"GameState\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"previousOwner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"newOwner\", \"type\": \"address\" } ], \"name\": \"OwnershipTransferred\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"_joinedPlayer\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"PlayerJoined\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"requestId\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"RequestedRandomness\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_currentRoundNumber\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"RevealEnded\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_currentRoundNumber\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"RevealStarted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_currentRoundNumber\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"RoundEnded\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_currentRoundNumber\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"RoundStarted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"_roundWinner\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_rewardAmount\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddr\", \"type\": \"address\" } ], \"name\": \"RoundWinner\", \"type\": \"event\" }, { \"inputs\": [], \"name\": \"CurrentRound\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"GameFeePool\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"GameWinningPool\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"MaxBets\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"RoundDuration\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"RoundStartTime\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"TicketPrice\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"TotalRounds\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"USDTicketPrice\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"betSize\", \"type\": \"uint256\" } ], \"name\": \"bet\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"name\": \"blocksWon\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes\", \"name\": \"\", \"type\": \"bytes\" } ], \"name\": \"checkUpkeep\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"upkeepNeeded\", \"type\": \"bool\" }, { \"internalType\": \"bytes\", \"name\": \"\", \"type\": \"bytes\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"chooseWinnerForCurrentRound\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"count\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"finalBalance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"gameCode\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"gameId\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"gameWinner\", \"outputs\": [ { \"internalType\": \"address payable\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"game_state\", \"outputs\": [ { \"internalType\": \"enum cryptoJengaV6.GAME_STATE\", \"name\": \"\", \"type\": \"uint8\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getContractBalance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getGameState\", \"outputs\": [ { \"internalType\": \"enum cryptoJengaV6.GAME_STATE\", \"name\": \"\", \"type\": \"uint8\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getLinkBalance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getNumberofPlayers\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getPlayerAddresses\", \"outputs\": [ { \"internalType\": \"address payable[]\", \"name\": \"\", \"type\": \"address[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getRoundRemainingTime\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"playerAddress\", \"type\": \"address\" } ], \"name\": \"getTokensLeft\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getUSDTicketPrice\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"isGameStateOpen\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"playerAddress\", \"type\": \"address\" } ], \"name\": \"isPlayerJoined\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"joinGame\", \"outputs\": [], \"stateMutability\": \"payable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"owner\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"participants\", \"outputs\": [ { \"internalType\": \"address payable\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes\", \"name\": \"\", \"type\": \"bytes\" } ], \"name\": \"performUpkeep\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"players\", \"outputs\": [ { \"internalType\": \"address payable\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"poolRewards\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"randomness\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"requestId\", \"type\": \"uint256\" }, { \"internalType\": \"uint256[]\", \"name\": \"randomWords\", \"type\": \"uint256[]\" } ], \"name\": \"rawFulfillRandomWords\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"renounceOwnership\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"_TotalRounds\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_MaxBets\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_RoundDuration\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_USDTicketPrice\", \"type\": \"uint256\" } ], \"name\": \"resetGameState\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"s_requestId\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint32\", \"name\": \"_callbackGasLimit\", \"type\": \"uint32\" } ], \"name\": \"setCallbackGasLimit\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"startGame\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"newOwner\", \"type\": \"address\" } ], \"name\": \"transferOwnership\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"withdrawEth\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" } ], \"name\": \"withdrawLINK\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";
    public static string gameAddress;

    // set chain: ethereum, moonbeam, polygon etc
    public const string chain = "ethereum";
    // set network mainnet, testnet
    public const string network = "goerli";

    public static IEnumerator createGame(Connection connection, string USDTicketPrice, int roundDurationSec, int totalRounds, int maxBets, string gameCode)
    {
        string method = "createGame";

        // args address _player, uint256 _EthTicketPrice
        string args = "[\"" + USDTicketPrice + "\"," + roundDurationSec + "," + totalRounds + "," + maxBets + ",\"" + gameCode + "\"]"; // TODO: Change this when calling the real contract
        // value in wei
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        string value = "0";

        Task createContractTask = Web3GL.SendContract(method, gameFactoryAbi, gameFactoryAddress, args, value, gasLimit, gasPrice);
        yield return new WaitUntil(() => createContractTask.IsCompleted);
        Debug.Log("completed create contract. Joining game now");


        Debug.Log("start 30 sec delay");
        yield return new WaitForSeconds(30);
        Debug.Log("end 30 sec delay");

        Task<string> joinGameTask = joinGame(gameCode);
        yield return new WaitUntil(() => joinGameTask.IsCompleted);
        Debug.Log("completed joining game");
        Debug.Log("game address is " + joinGameTask.Result);
        gameAddress = joinGameTask.Result;

        Debug.Log("game address is " + gameAddress);
        //SceneManager.LoadScene("GameLogin");
        SceneManager.LoadScene("Lobby");
    }

    public static async Task<string> joinGame(string gameCode)
    {
        try
        {
            Connection connection = GameObject.Find("Network").GetComponent<Connection>();
            Debug.Log("get game address using code " + gameCode);
            string method = "getGameAddress";
            string args = "[\"" + gameCode + "\"]";
            string response = await EVM.Call(chain, network, gameFactoryAddress, gameFactoryAbi, method, args);
            gameAddress = response;
            Debug.Log("Sucessfully Joining an existing game");
            connection.SendInitGameMessage("{ \"name\": \"address\", \"value\": \"" + gameAddress + "\"}");
            return gameAddress;
        }
        catch (Exception e)
        {
            Debug.Log("That game does not exist " + e.Message);
            return null;
        }
    }
}
#endif