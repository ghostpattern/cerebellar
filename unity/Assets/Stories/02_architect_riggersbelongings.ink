=== architect_riggersbelongings ===

I stand at the door of the rigger's place.

* [Knock.] sync:knock_on_the_door
* [Open the door.] sync:open_the_door
- -> DONE

= knock_on_the_door_we

We knock at the door.

-> knock_on_the_door_continue

= knock_on_the_door_i

I knock at the door.

-> knock_on_the_door_continue

= knock_on_the_door_they

 I assume they'll just open the door, but they knock instead. Is it locked? Did they forget their keys?

-> knock_on_the_door_continue

= knock_on_the_door_continue

We wait around while the sound of the knock fades down the empty corridor.

The door suddenly slides open. Someone is standing on the other side, and they beckon us in.

-> meeting_reef

= open_the_door_we

We slide the door open.

-> open_the_door_continue

= open_the_door_i

I slide the door open.

-> open_the_door_continue

= open_the_door_they

They slide the door open.

-> open_the_door_continue

= open_the_door_continue

I hear movement from further in the house, then someone walks out of the back room toward us.

-> meeting_reef

= meeting_reef

We are standing in a small kitchen. It can't be more than five feet in either direction. There is a curious array of jars filled with pickled foods that I'm unable to identify. The rest of the kitchen space is covered in unclean pots and pans. A layer of grime rests in the cracks and corners of the kitchen. I don't want to touch anything.

The stranger asks, "Are you okay?"

* [Let them answer.]
- sync

"It's strange," we reply. "I'm okay. We're okay, I think."

They look at me strangely and say, "I'm Reef, by the way," extending their hand.

* [Reef, huh?]
- sync

We shake the hand offered.

They're still looking at us as we say, "We're here to move my stuff."

They blink. "Sure, uh. Go right ahead. I packed your things. I wasn't sure if it would be difficult... after."

We walk past Reef and into the bedroom, which thankfully is a little larger than the kitchen. The room has two beds, and is otherwise sparsely decorated. Each bed has a shelf above it, one covered in trinkets, the other bare. A bag sits on the bed under the empty shelf.

From over our shoulder Reef says, "I wasn't sure if you'd want the holoframe, but I think you should have it."

Sitting beside the bag is a box for projecting holographic displays of photos. I own a few, but none so cheaply made. This particular brand has a history of igniting, but uh, what's the price of a memory?

* [Take the holoframe.] sync:take_frame
* [Leave it.] sync:leave_frame
- -> DONE

= take_frame_we

We take the holoframe. Maybe we can transfer the photos to one of mine.

-> take_frame_continue

= take_frame_i

I pick up the holoframe. Maybe we can transfer the photos to one of mine.

-> take_frame_continue

= take_frame_they

I thought it best not to take an object with a history of spontaneous combustion, but they have a different idea. They place the device in their bag.

-> take_frame_continue

= take_frame_continue

-> leave_home

= leave_frame_we

"Keep it, Reef. It's - it's yours now." We leave the holoframe.

-> leave_frame_continue

= leave_frame_i

I leave the holoframe. Fire and their memories, I need neither in my home.

-> leave_frame_continue

= leave_frame_they

"Keep it, Reef. It's - it's yours now." Surprising me, they decide to leave the box on the bed.

-> leave_frame_continue

= leave_frame_continue

-> leave_home

= leave_home

With their bag in hand, we head for the door. Neither of us is inclined to spend much more time there.

* [Two weeks later.]

- sync

-> END