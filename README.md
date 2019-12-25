# LiteComponent
## How to install
Add in `Packages/manifest.json` to `dependencies`:
```javascript
"com.danand.litecomponent": "https://github.com/Danand/LiteComponent.git#0.1.0-package-unity"
```
## How to use
Just add attribute to interface-typed field to draw serializible class in inspector.
```csharp
[LiteComponent]
public ICustomBehaviour behaviour;
```