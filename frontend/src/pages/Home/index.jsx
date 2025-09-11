import { useState } from "react";
import LeftSidebar from "../../features/home/components/Sidebar/LeftSidebar";
import RightSidebar from "../../features/home/components/Sidebar/RightSidebar";
import Post from "../../features/home/components/Post/Post";
import CreatePost from "../../features/home/components/Post/CreatePost";
import CreatePostPopover from "../../features/home/components/Popover/CreatePost/index";
const mockFriends = [
  { id: 2, name: 'Sarah Johnson', avatar: 'https://images.unsplash.com/photo-1441974231531-c6227db76b6e?w=40&h=40&fit=crop&crop=face' },
  { id: 3, name: 'John Doe', avatar: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=32&h=32&fit=crop&crop=face' },
  { id: 4, name: 'Emma Wilson', avatar: 'https://images.unsplash.com/photo-1418065460487-3cd7cc6cd987?w=32&h=32&fit=crop&crop=face' },
  { id: 5, name: 'Mike Chen', avatar: 'https://images.unsplash.com/photo-1506197603052-3cc9c3a201bd?w=32&h=32&fit=crop&crop=face' },
];

const currentUser = {
  id: 1,
  name: 'You',
  avatar: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=32&h=32&fit=crop&crop=face'
}

const posts = [
  {
    id: 1,
    user: mockFriends[0],
    content: 'Morning hike through the forest! Nothing beats the fresh mountain air 🌲\n\nFound this amazing trail that leads to a hidden waterfall. Nature therapy at its finest! 🏔️',
    image: 'https://images.unsplash.com/photo-1441974231531-c6227db76b6e?w=500&auto=format&fit=crop&q=60',
    timestamp: '2 hours ago',
    reactions: { '👍': { count: 12 }, '❤️': { count: 8 }, '🤗': { count: 3 } },
    userReaction: null,
    comments: [
      {
        id: 1,
        user: mockFriends[1],
        content: 'What a stunning view! Need to check this trail out! 🌿',
        timestamp: '1 hour ago',
        reactions: {},
        userReaction: null,
        replies: [
          {
            id: 11,
            user: currentUser,
            content: 'Thanks!',
            timestamp: '30 min ago',
            reactions: { '❤️': { count: 1 } },
            userReaction: null,
            replies: []
          }
        ]
      }
    ]
  },
  {
    id: 2,
    user: currentUser,
    content: 'Peaceful lake reflection during golden hour! 🌅',
    image: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=500&auto=format&fit=crop&q=60',
    timestamp: '4 hours ago',
    reactions: { '❤️': { count: 15 }, '😮': { count: 5 } },
    userReaction: null,
    comments: []
  },
  {
    id: 3,
    user: mockFriends[2],
    content: 'Discovered this beautiful wildflower meadow! Spring is finally here 🌸',
    image: 'https://images.unsplash.com/photo-1490750967868-88aa4486c946?w=500&auto=format&fit=crop&q=60',
    timestamp: '6 hours ago',
    reactions: { '👍': { count: 7 }, '😂': { count: 2 } },
    userReaction: null,
    comments: [{ id: 2, user: currentUser, content: 'So colorful!', timestamp: '5 hours ago', reactions: {}, userReaction: null, replies: [] }]
  },
];
export default function Home() {
  const [openCreatePostPopover, setCreatePostPopover] = useState(false);
  const openPostModal = () => {
    setCreatePostPopover(true);
  };

  return (
    <div className="min-h-screen bg-white">


      <div className="pt-14">
        <div className="max-w-screen-2xl mx-auto flex">
          <LeftSidebar currentUser={currentUser} />

          <main className="flex-1 md:mx-4 lg:ml-80 lg:mr-80">
            <div className=" p-2 sm:p-4 space-y-4">
              <CreatePost currentUser={currentUser} onOpenModal={openPostModal} />
              <div className="space-y-4">
                {posts.map((post, index) => (
                  <Post
                    key={index}
                    post={post}
                    currentUser={currentUser}
                  />
                ))}
              </div>
            </div>
          </main>
          <RightSidebar />
        </div>
      </div>

      <CreatePostPopover handleClose={() => setCreatePostPopover(false)} open={openCreatePostPopover} />


    </div>
  );
}