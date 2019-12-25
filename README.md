# LiteComponent
## How to install
Add in `Packages/manifest.json` to `dependencies`:
```javascript
"com.danand.litecomponent": "https://github.com/Danand/LiteComponent.git#0.1.0-package-unity"
```
## How to use
Just add attribute `LiteComponent` to interface-typed field for drawing implementations dropdown and selected implementation's fields in inspector:
```csharp
[SerializeReference]
[LiteComponent]
public ICustomBehaviour behaviour;
```
Notice that Unity 2019.3 is required because of using `SerializeReference` for abstract types serialization.

Mark every behaviour implementation as `Serializable`:
```csharp
[Serializable]
public class ICustomBehaviourImpl : ICustomBehaviour { /* ... */ }
```
