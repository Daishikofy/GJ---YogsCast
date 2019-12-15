using UnityEngine;
public interface Selectable
{
    void selected();
    void deSelected();
    string getName();
    Sprite getSprite();
    bool isGroundFriendly();
    bool isType(string type);
}
