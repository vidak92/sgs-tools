# SGS Tools

A small, utility-style library for Unity.

## Highlights

### Extension Methods
Convenience methods for some of the most common components and primitive types, e.g:

```
transform.SetLocalPositionX(0f); // set transform.localPosition.x to 0
transform.AddPositionXY(1f, 1f); // move transform by 1 unit along the X and Y axes

// ...

spriteRenderer.SetAlpha(0.5f); // set spriteRenderer.color.a to 0.5
var fullyOpaqueSpriteColor = spriteRenderer.color.WithAlpha(1f);

// ...

var list = new List<T>() { ... };
var randomElement = list.GetRandomElement(); // get an element at a random index or default(T)
list.Shuffle(); // rearrange all elements
```

### DebugDraw
- A utility for drawing basic UI shapes, similar to `Gizmos` and `Handles`, that works in the Game view and built/deployed games
- Uses an immediate-mode style API, all drawn shapes are cleared after each `Update()`
- Supports drawing 2D shapes in world space: Line, Rect and Circle
```
private void Awake()
{
    // setup, can be called from anywhere
    DebugDraw.IsEnabled = true;
    
    // default values, they don't have to be set explicitly but can be overridden
    DebugDraw.Settings.SortLayerName = "Default";
    DebugDraw.Settings.StartingSortOrder = 1000;
    DebugDraw.Settings.LineWidth = 0.5f;
    DebugDraw.Settings.DefaultColor = Color.white;
}

private void Update()
{
    Vector3 centerPosition = ...;
    float radius = ...;
    DebugDraw.DrawCircle(centerPosition, radius); // use default color
    
    // ...
    
    Vector3 position1 = ...;
    Vector3 position2 = ...;
    DebugDraw.DrawLine(position1, position2, Color.red);
    
    // ...
    
    Vector3 centerPosition = ...;
    Vector2 rectSize = ...;
    
    float defaultWidth = DebugDraw.Settings.LineWidth;
    DebugDraw.Settings.LineWidth = ...; // custom line width
    
    DebugDraw.DrawRect(centerPosition, rectSize, Color.green);
    
    DebugDraw.Settings.LineWidth = defaultWidth; // reset line width  
}
```
### ObjectPool
- A reusable class for pooling objects
- Works with any class that derives from `UnityEngine.Component`

Example usage:
```
public ProjectilePrefab ProjectilePrefab;
public Transform ProjectileParent;

private ObjectPool<Projectile> _projectilePool;

private void Awake()
{
    // init pool with an existing parent object for pooled objects
    _projectilePool = new ObjectPool<Projectile>(ProjectilePrefab, 100, ProjectileParent);
    
    // alternatively, init pool and create a new parent object in the scene with a given name
    _projectilePool = ObjectPool<Projectile>.CreateWithGameObject(ProjectilePrefab, 100, "ProjectilePool");
}

private Projectile SpawnProjectile()
{
    var projectile = _projectilePool.Get();
    
    // setup projectile here...
    
    return projectile;
}

private void DespawnProjectile(Projectile projectile)
{
    _projectilePool.Return(projectile);
}
```

### Shaders
- This library contains some shaders for basic post-processing effects like Blur, Vignette and Fisheye
- Also contains some helper methods for creating some basic 2D SDFs like Circle and Capsule

```
float4 frag (v2f i) : SV_Target
{
    float2 center = ...;
    float radius = ...;
    float circleSDF = SDF_Circle(i.uv, center, radius);
    
    // ...
    
    float4 circleColor = ...; 
    float circle = CircleFill(i.uv, center, radius); // uses SDF_Circle behind the scenes
    float circleColor = circle * circleColor; 
    
    // ...
}
```

### Misc

#### CoroutineUtils
- Helper methods for easily calling coroutines
- Uses a single `GameObject` to start the coroutines behind the scenes
- Can be called from anywhere within the `Update` loop
```
CoroutineUtils.CallAfterOneFrame(() => { ... });
CoroutineUtils.CallAfterXFrames(5, () => { ... });
CoroutineUtils.CallAfterXSeconds(1f, () => { ... });
```

#### ObjectShaker
- A component that can be used shaking objects, useful for screenshake effects
- Parameters like shake magnitude and duration are exposed in the Inspector
```
var cameraShaker = Camera.main.GetComponent<ObjectShaker>();
cameraShaker.StartShake(intensity: 0.5f); // shake camera with intensity in [0-1] range
// ...
cameraShaker.StopShake();  
```

#### FloatRange and IntRange
- Custom data types for easily working with number ranges
- Exposed in the Inspector 
```
// Example for a sound effect

public FloatRange VolumeRange; // contains Min and Max fields

public PlaySoundWithRandomVolume()
{
    float volume = VolumeRange.GetRandom();
    // ...
}

public PlaySoundWithIntensity(float intensity) // [0-1] intensity range
{
    float volume = VolumeRange.GetValueAt(intensity);
    // ...
}
```

## Usage

Available as a [UPM](https://docs.unity3d.com/Manual/Packages.html) package (via git).

## Notes

This library is used for internal projects at [Smiling Goat Studios](https://www.smilinggoatstudios.com). The API hasn't stabilized yet and breaking changes are made often. 