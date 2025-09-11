import { ChatBubbleLeftEllipsisIcon } from "@heroicons/react/24/outline";

const MessageFriend = ({ friend }) => {

    return <button
        className="btn-primary flex flex-1 items-center justify-center gap-2 text-sm"
        onClick={() => { }}
    >
        <ChatBubbleLeftEllipsisIcon className="h-4 w-4" />
        <span className="hidden sm:inline">Message</span>
    </button>
}
export default MessageFriend;