=== architect_walkinthepark ===

As we stand before the entrance to the park, I am already starting to relax. I have been looking forward to this for so many months.

The synchronisation was a stressful process. It is still stressful. We share a space, but we're rarely of the same mind. Sometimes I can guess at how they might act; sometimes my body moves unexpectedly, possessed. Sometimes, even now, we fight for control.

Today, we can both relax.

* [Pay for the park.]
- sync

We swipe our credits at the gate, and the door slides open to let us past. A weight lifts from me.

We pause just past the gate, so that they can take it all in.

The shimmer of a light breeze sweeps across fields of trimmed grass beneath an azure sky. Vivid splashes of pink, orange and white punctuate the landscape in perfectly manicured garden beds. A gravel path lazily weaves in and around the fields and flowers, finally straying into a line of trees. Beautiful. Peaceful. Spacious.

The space is worth the money we paid. The view makes it a bargain.

And the air! We take a deep, savoring breath.

* [Start walking.]
- sync

We stroll down the path. I marvel at the warmth of the artificial sunlight, as it radiates through to our bones. The gravel crunches under each step.

The path leads us beside a garden bed, where we stop to watch a handful of bees play amongst the flowers. Have they seen a bee before? Probably not. They must be impressed.

* [Continue walking.]
- sync

Our walk along the path continues. A stream intersects the road at a small stone bridge, winding along beside us a while before cascading over a minor drop. Ribbons of colour dance in the spray.

* [We're disrupted--]
- sync

We're disrupted by a message that projects just ahead of us and reads, "Your park session is about to expire. Would you like to extend it?" This is way too early - we haven't yet reached the oak!

* [Agree to extend the session.] sync:park_extended
* [Reluctantly proceed to the nearest park exit.] sync:exit_park
- -> DONE

= exit_park_we

With my extended leave from work still ongoing, the second session was hard to justify. The government welfare we'd been receiving doesn't reach as far as my wage would've. We walk briskly toward the nearest park exit. We take one final deep breath, and step back into the real world.

-> park_end

= exit_park_i

With my extended leave from work still ongoing, the second session was hard to justify. I grasp for a reason to stay, but find nothing. The government welfare just doesn't reach as far as my old wage. I walk us over to the nearest exit. I draw in one last breath, and take us back to the real world.

-> park_end

= exit_park_they

Of course, I want to stay, but I can't speak the words. I'm not in control. They start walking, and soon we are back at the last exit we had seen. Without a moment of hesitation, they step us back into the real world.

-> park_end

= park_extended_we

We agree to stay a little longer, and the projection indicates that the credits have been drawn from my bank account.

-> park_extended_continue

= park_extended_i

I agree to stay a little longer, and the projection indicates that the credits have been drawn from my bank account. They wouldn't want to miss the oak tree.

-> park_extended_continue

= park_extended_they

With my extended leave from work still ongoing, the second session was hard to justify. They disagree, requesting the extended session. The projection indicates that the credits have been drawn from my bank account.

-> park_extended_continue

= park_extended_continue

* [Take off our shoes.]
- sync

Tired of the constraints of the path, we kick off our shoes and step out onto the grass.

-> park_end

= park_end

* [Before the new year.]

- sync

-> END