=== rigger_walkinthepark ===

We stand before the entrance of the park. A partially-simulated leisure experience, designed to present a classic vision of the outside world. And this is the largest park in the city.

I would never be able to afford this. Not even a half-fare. Our 'two for the price of one' government-issued deal doesn't ease my mind on the cost. How many excursions like this can we even afford?

* [Pay for the park.]
- sync

The number that slides onto the payment screen is startling. We pay it without hesitation, and the door opens.

We enter, then we stop.

I gaze out at the vista before me. Sweeping hills, covered in grass. The clear blue sky. Flowers of pink, orange and white. It doesn't look real. I wonder how much of it is.

As my eyes cover the landscape, each sight seems more expensive than the last. I begin to notice my breathing, and calculate the cost of each inhalation taken in this space.

* [Start walking.]
- sync

We start walking down the path, and the gravel crunches under each footfall.

I fixate on the flowers in an approaching garden bed. I haven't seen a real flower before, only ever in holos, and I marvel at the variety of colour. I wonder what each one is called. As we get closer, I notice little bees, darting between the flower heads. I had heard that bees were extinct, and wonder if the buzzing I hear is a historical recording or the whirring of a tiny motor.

* [Continue walking.]
- sync

We continue to walk the path, and I feel myself warming to the experience. Or maybe it's just the artificial sunlight.

Soon after, we cross a small stone bridge over a stream. The path leads us next to the crystal-clear water for the next fifteen minutes. A highlight is when the stream runs over a minor drop, and colours dance in the spray.

* [We're disrupted--]
- sync

We're disrupted by an alert that hovers in the air before us. It reads, "Your park session is about to expire. Would you like to extend it?"

* [We've already spent more than enough. Let's go.] sync:exit_park
* [It's so expensive, but so worthwhile. Let's stay.] sync:park_extended
- -> DONE

= exit_park_we

We head back to the nearest exit. I compare the experience to what we paid for it. It's obvious that the cost is mostly there for exclusivity. To keep the space empty.

I get the space I need up in the wires.

-> park_end

= exit_park_i

I head back to the nearest exit. I wonder if it was worth what we had already paid. It was obvious that the cost is mostly there for exclusivity. To keep the space empty.

I get the space I need up in the wires.

-> park_end

= exit_park_they

I'd like to stay a while longer, but they start walking toward the exit.

-> park_end

= park_extended_we

We agree to stay longer, and the projection indicates that, once again, a startling number of credits have been paid.

-> park_extended_continue

= park_extended_i

I agree to stay longer, and the projection indicates that, once again, a startling number of credits have been transferred from their account.

-> park_extended_continue

= park_extended_they

I want to leave, but they agree to stay. The projection indicates that, once again, a startling number of credits have been paid. I feel a bit sick, but try to ward off the sensation; now that it's done, I may as well enjoy myself.

-> park_extended_continue

= park_extended_continue

* [Take off our shoes.]
- sync

We look down at the path in front of us, and after a moment of hesitation, we kick off our shoes and step out onto the grass.

-> park_end

= park_end

* [Before the new year.]

- sync

-> END