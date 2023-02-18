using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgent {
    void attack();
    void Dialogue();
    void Inspect();
    void Pickup();
    void Interact();
}
