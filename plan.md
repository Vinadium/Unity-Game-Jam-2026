# Game Design Plan

*Working title: TBD — Unity Game Jam 2026*

## High Concept

A **roguelike + idler hybrid**. A human knight idly farms a dragon for scales, which
cascade down a **pachinko board** into a **physical weighing scale**. Fill the scale to
advance. Meanwhile, the player runs **roguelike runs** to gather food that keeps the
knight fighting and to earn upgrades. The two halves feed each other in a continuous
cyclical loop.

## The Two Loops

### 1. The Idler (Passive / Always Running)

- A human **knight** starts with a humble **stick** and attacks a **dragon**.
- Each hit makes the dragon **drop scales**.
- Scales fall into a **pachinko board** and bounce down through pegs.
- At the bottom, scales pass through **multiplier zones** that increase their value.
- Scales land in a **physical weighing scale** (a real, tilting balance).
- When enough weight/value accumulates on the scale, the player can **advance to the
  next level**.
- **Fuel dependency:** the knight needs **food** to keep swinging. If he runs out of
  food, he **stops attacking** and no scales drop — the idler stalls until refueled.

### 2. The Roguelike (Active / Player-Driven)

- The player goes on **runs** to **collect food** that powers the idler knight.
- Runs also yield **upgrades for the idler** (better weapons than the stick, faster
  attacks, better dragon, more scales per hit, etc.).
- The roguelike is where the player spends active attention while the idler ticks along.

## The Cyclical Feed (Core Hook)

The design's heart is that **each mode upgrades the other**:

- **Roguelike → Idler:** food to keep the knight running + upgrades that boost scale
  output.
- **Idler → Roguelike:** scales/progress unlock **upgrades for the roguelike** (stronger
  character, better loot, new abilities).

This creates a self-reinforcing gameplay loop where progress in one side accelerates the
other.

## Key Mechanics to Prototype

| System | Notes |
| --- | --- |
| Knight auto-attack | Attack speed & damage scale with upgrades; gated by food |
| Dragon scale drops | Scales spawn as physics objects on each hit |
| Pachinko board | Peg field with satisfying bounce physics |
| Multiplier zones | Bottom slots that scale value (x2, x3, etc.) |
| Weighing scale | Physical balance that tilts; fills a "weight quota" per level |
| Food / fuel system | Depletes over time; empty = knight idle |
| Roguelike runs | Food + idler-upgrade drops |
| Upgrade cross-feed | Idler progress unlocks roguelike upgrades and vice versa |
| Level progression | Advance when scale target is met |

## Open Questions / To Decide

- **Roguelike genre specifics:** top-down dungeon crawl? Auto-battler? Card-based?
- **Food economy:** timer-based drain, per-hit drain, or hybrid?
- **Scale "win" condition:** raw weight, total value, or balance tipping past a point?
- **Multiplier design:** fixed slots vs. randomized vs. player-placed pegs?
- **Meta-progression:** what persists between roguelike runs?
- **Theme & art direction:** medieval fantasy tone, but stylized how?

## Jam Scope Priorities (MVP first)

1. Knight attacks dragon → scales drop.
2. Scales fall through pachinko → land on scale → level-up threshold.
3. Food fuel gate on the knight.
4. A minimal roguelike loop that produces food + one upgrade type.
5. One example of the cyclical feed working end-to-end.

*Polish (multipliers, more upgrades, art, juice) comes after the core loop is fun.*
