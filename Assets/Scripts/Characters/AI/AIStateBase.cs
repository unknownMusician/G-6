using UnityEngine;
using System.Collections;

public interface AIStateBase {

    void OnStateStart();
    void StateUpdate();
    void OnStateEnd();
}
