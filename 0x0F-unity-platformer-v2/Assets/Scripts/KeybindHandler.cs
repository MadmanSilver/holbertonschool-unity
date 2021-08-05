using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindHandler : MonoBehaviour
{
    public Dictionary<string, KeyCode> defaults = new Dictionary<string, KeyCode>() {{"Forward", KeyCode.W}, {"Left", KeyCode.A}, {"Right", KeyCode.D}, {"Backward", KeyCode.S}, {"Jump", KeyCode.Space}, {"Rotate", KeyCode.E}};
    public Text[] texts;

    public Text recording = null;
    private Dictionary<string, KeyCode> changes = new Dictionary<string, KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Text t in texts) {
            t.text = PlayerPrefs.GetString(t.text);
        }
    }

    void OnGUI() {
        if (recording != null) {
            Event e = Event.current;

            if (e.isKey) {
                changes[recording.gameObject.name] = e.keyCode;
                recording.text = e.keyCode.ToString();
                recording = null;
            }
        }
    }

    public void Apply() {
        foreach (KeyValuePair<string, KeyCode> pair in changes) {
            PlayerPrefs.SetString(pair.Key, pair.Value.ToString());
        }
    }

    public void Reset() {
        foreach (KeyValuePair<string, KeyCode> pair in defaults) {
            changes[pair.Key] = pair.Value;
            foreach (Text t in texts) {
                if (t.gameObject.name == pair.Key)
                    t.text = pair.Value.ToString();
            }
        }
    }

    public void SetRecording(Text keybind) {
        recording = keybind;
    }
}
