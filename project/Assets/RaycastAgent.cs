using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.ThirdPerson;

// RaycastAgent
public class RaycastAgent : Agent
{
    Rigidbody rBody;

    private ThirdPersonCharacter character;
    private Transform cam;

    // 初期化時に呼ばれる
    public override void Initialize()
    {
        this.cam = Camera.main.transform;
        this.rBody = GetComponent<Rigidbody>();
        this.character = GetComponent<ThirdPersonCharacter>();
    }

    // エピソード開始時に呼ばれる
    public override void OnEpisodeBegin()
    {
        // 周回数のリセット
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(0.0f, 0.0f, 7.5f);
        this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
    }

    // 観察取得時に呼ばれる
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.rotation);
        sensor.AddObservation(this.rBody.velocity);
    }

    // 行動実行時に呼ばれる
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        ActionSegment<int> actions = actionBuffers.DiscreteActions;

        float v = 0.0f, h = 0.0f;
        bool j = false;
        if (actions[0] == 1) v += 1.0f;
        if (actions[1] == 1) v -= 1.0f;
        if (actions[2] == 1) h -= 1.0f;
        if (actions[3] == 1) h += 1.0f;
        if (actions[4] == 1) j = true;

        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        // Vector3 move = v*camForward + h*cam.right;
        Vector3 move = v*Vector3.forward + h*Vector3.right;

        character.Move(move, false, j);

        // ステップ毎の報酬
        AddReward(-0.1f);
    }

    public void Wall()
    {
        AddReward(-10.0f);
    }

    public void PanelCheckPoint()
    {
        // AddReward(0.2f);
    }

    public void AntiPanelCheckPoint()
    {
        // AddReward(-1.0f);
    }

    public void StepCheckPoint()
    {
        // AddReward(0.5f);
    }

    public void AntiStepCheckPoint()
    {
        // AddReward(-1.0f);
    }

    public void Goal()
    {
        Debug.Log("Goal!");
        AddReward(200.0f);
        EndEpisode();
    }

    // ヒューリスティックモードの行動決定時に呼ばれる
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> actions = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.UpArrow)) actions[0] = 1;
        if (Input.GetKey(KeyCode.DownArrow)) actions[1] = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) actions[2] = 1;
        if (Input.GetKey(KeyCode.RightArrow)) actions[3] = 1;
        if (Input.GetKey(KeyCode.Space)) actions[4] = 1;
    }
}
