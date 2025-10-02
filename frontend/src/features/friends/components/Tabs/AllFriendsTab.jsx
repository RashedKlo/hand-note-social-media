import { useEffect, useState } from "react";
import FriendCard from "../Card/FriendCard";
import GetEmptyStateContent from "../EmptyState/GetEmptyStateContent";
import { useLoadFriends } from "../../hooks/useLoadFriends";
import LoadingCard from "../Loading/LoadingCard";
import EmptyState from "../EmptyState/EmptyState";
import { useSearchUserFriends } from "../../hooks/useSearchUserFriends";
import SearchInput from "../../../../components/common/Input/SearchInput";

const AllFriendsTab = () => {
    const [searchTerm, setSearchTerm] = useState('');

    // Rename conflicting properties using aliases
    const {
        friends: loadedFriends,
        isLoading: isLoadingFriends,
        loadFriends
    } = useLoadFriends();

    const {
        searchFriends,
        friends: searchedFriends,
        isLoading: isSearching
    } = useSearchUserFriends();

    // Determine which data to display
    const friends = (searchTerm ? searchedFriends : loadedFriends) || [];
    const isLoading = (searchTerm ? isSearching : isLoadingFriends) || false;

    useEffect(() => {
        if (!searchTerm) {
            loadFriends(1000, 1, 10);
        }
        else
            searchFriends(searchTerm, 1, 10);

    }, [searchTerm, loadFriends, searchFriends]);

    // Renders loading cards while the data is being fetched
    if (isLoading) {
        return (
            <div className="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
                <div className="grid gap-4 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4">
                    {[...Array(6)].map((_, index) => (
                        <LoadingCard key={index} />
                    ))}
                </div>
            </div>
        );
    }

    return (
        <div className="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
            <div className="mb-6">
                <div className="mb-2 flex items-center justify-between">
                    <h2 className="text-lg font-bold text-gray-900 sm:text-xl">All Friends</h2>
                    <div className="flex items-center gap-2">
                        <span className="badge bg-gray-100 text-gray-600">
                            {friends?.length || 0} {friends?.length === 1 ? 'person' : 'people'}
                        </span>
                    </div>
                </div>
                <div className="flex justify-between items-center flex-col md:flex-row gap-2">
                    <p className="text-sm text-gray-500 sm:text-base hidden md:block">Stay connected with your friends</p>
                    {/* Search input */}
                    <SearchInput placeholder={"Search friends..."} value={searchTerm} onSearchChange={(e) => setSearchTerm(e.target?.value)} onSearchClick={() => { }} />
                </div>
            </div>

            {!friends || friends.length === 0 ? (
                <EmptyState
                    title={GetEmptyStateContent('friends')?.title}
                    description={GetEmptyStateContent("friends")?.description}
                    Icon={GetEmptyStateContent("friends")?.icon}
                />
            ) : (
                <div className="grid gap-4 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4">
                    {friends.map((person) => (
                        <FriendCard
                            key={person?.id}
                            friend={person}
                        />
                    ))}
                </div>
            )}
        </div>
    );
}

export default AllFriendsTab;