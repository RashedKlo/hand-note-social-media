export default function BackgroundElements ()  {
   return <>
        {/* Background gradient overlay */}
        <div 
            className="absolute inset-0 pointer-events-none"
            style={{
                background: 'radial-gradient(circle at 20% 50%, rgba(120, 119, 198, 0.3) 0%, transparent 50%), radial-gradient(circle at 80% 20%, rgba(255, 255, 255, 0.1) 0%, transparent 50%)'
            }}
        />
        
        {/* Floating circle 1 */}
        <div className="absolute top-[10%] left-[5%] w-15 h-15 sm:w-20 sm:h-20 md:w-25 md:h-25 rounded-full bg-white/10 backdrop-blur-sm animate-float" />
        
        {/* Floating circle 2 */}
        <div className="absolute bottom-[20%] right-[5%] w-10 h-10 sm:w-12 sm:h-12 md:w-15 md:h-15 rounded-full bg-white/5 backdrop-blur-sm animate-float-reverse" />
        
      
    </>
};